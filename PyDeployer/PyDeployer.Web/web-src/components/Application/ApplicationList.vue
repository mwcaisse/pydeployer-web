<template>
    <div>
        <h2 class="title">Applications</h2>
        <div class="box">
            <ul>
                <li class="box" v-for="application in applications">
                    {{application.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="fa-clone" :action="true"></app-icon>
                        <app-icon icon="fa-trash" :action="true"></app-icon>
                        <app-icon icon="fa-chevron-down" :action="true"></app-icon>
                    </span>
                </li>
            </ul>
        </div>
    </div>    
</template>

<script>
    import { ApplicationService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"

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
            }
        },
        created: function () {
            this.fetchApplications();
        },
        components: {
            "app-icon": Icon
        }
    }
</script>

<style scoped>
    .action-icon:hover { 
        transform:scale(1.5);
        cursor: pointer;
    }
</style>