<template>
    <div>        
        <div class="box">
            <p class="subtitle">
                Applications
                <span class="is-pulled-right">
                    <app-icon icon="fa-plus" :action="true" v-on:click.native="create"></app-icon>
                </span>
            </p>
            <ul v-if="applications.length > 0">
                <li class="box" v-for="application in applications">
                    {{application.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="fa-clone" :action="true"></app-icon>
                        <app-icon icon="fa-trash" :action="true" v-on:click.native="deleteApplication(application)"></app-icon>                     
                    </span>
                </li>
            </ul>
            <p v-else class="has-text-centered">
                No applications exist yet!
            </p>
        </div>
        <application-modal></application-modal>
    </div>    
</template>

<script>
    import system from "services/System.js"
    import { ApplicationService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"
    import ApplicationModal from "components/Application/ApplicationModal.vue"

    export default {
        name: "application-list",
        data: function() {
            return {
                applications: []
            }
        },
        methods: {
            fetchApplications: function () {
                ApplicationService.getAll().then(function (data) {
                    this.applications = data;
                }.bind(this),
                function (error) {
                    console.log("Error fetching applications: " + error)
                });
            },
            create: function () {
                system.events.$emit("applicationModal:create");
            },
            deleteApplication: function (application) {
                ApplicationService.delete(application.applicationId).then(function (res) {
                    if (res) {
                        var index = this.applications.indexOf(application);
                        this.applications.splice(index, 1);
                    }
                    else {
                        console.log("Failed to delete application.");
                    }
                }.bind(this), function (error) {
                    console.log("Error deleting application: " + error)
                })
            }
        },
        created: function () {
            this.fetchApplications();

            system.events.$on("applicationModal:created", function (application) {
                this.applications.push(application);
            }.bind(this));

            system.events.$on("applicationModal:updated", function (application) {
                var index = this.applications.findIndex(function (elm) {
                    return elm.applicationId == application.applicationId;
                });
                if (index >= 0) {
                    this.applications.splice(index, 1, application);
                }
            }.bind(this));
        },
        components: {
            "app-icon": Icon,
            "application-modal": ApplicationModal
        }
    }
</script>
