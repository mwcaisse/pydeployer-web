<template>
    <div>
        <ul>
            <li class="box" v-for="application in applications">
                {{application.name}} ({{application.applicationUuid}})
            </li>   
        </ul>
    </div>
</template>

<script>
    import { ApplicationService } from "services/ApplicationProxy.js"

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
        }
    }
</script>