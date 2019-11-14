<template>
    <div>        
        <div class="box">
            <p class="subtitle">
                Applications
                <span class="is-pulled-right">
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="create"
                    />
                </span>
            </p>
            <ul v-if="applications.length > 0">
                <li
                    v-for="application in applications"
                    :key="application.applicationId"
                    class="box"
                >
                    {{ application.name }}
                    <span class="is-pulled-right">
                        <app-icon
                            icon="clone"
                            :action="true"
                            @click.native="viewApplication(application)"
                        />
                        <app-icon
                            icon="trash"
                            :action="true"
                            @click.native="deleteApplication(application)"
                        />                     
                    </span>
                </li>
            </ul>
            <p
                v-else
                class="has-text-centered"
            >
                No applications exist yet!
            </p>
        </div>
        <application-modal />
    </div>    
</template>

<script>
import system from "@app/services/System.js"
import Links from "@app/services/Links.js"
import {ApplicationService} from "@app/services/ApplicationProxy.js"
import Icon from "@app/components/Common/Icon.vue"
import ApplicationModal from "@app/components/Application/ApplicationModal.vue"

export default {
    name: "ApplicationList",
    components: {
        "app-icon": Icon,
        "application-modal": ApplicationModal
    },
    data: function() {
        return {
            applications: []
        }
    },
    created: function () {
        this.fetchApplications();

        system.events.$on("applicationModal:created", function (application) {
            this.applications.push(application);
        }.bind(this));

        system.events.$on("applicationModal:updated", function (application) {
            let index = this.applications.findIndex(function (elm) {
                return elm.applicationId == application.applicationId;
            });
            if (index >= 0) {
                this.applications.splice(index, 1, application);
            }
        }.bind(this));
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
                    let index = this.applications.indexOf(application);
                    this.applications.splice(index, 1);
                }
                else {
                    console.log("Failed to delete application.");
                }
            }.bind(this), function (error) {
                console.log("Error deleting application: " + error)
            })
        },
        viewApplication: function (application) {                
            window.location = Links.application(application.applicationId);
        }
    }
}
</script>
