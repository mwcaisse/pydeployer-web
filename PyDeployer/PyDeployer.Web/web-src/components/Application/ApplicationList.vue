<template>
    <div>
        <h2 class="title">Applications</h2>
        <div class="box">
            <ul>
                <li class="box" v-for="application in applications">
                    {{application.name}}
                    <span class="is-pulled-right">
                        <i class="fas fa-clone action-icon"></i>
                        <i class="fas fa-trash action-icon"></i>
                        <i class="fas fa-chevron-down action-icon"></i>
                    </span>
                </li>
            </ul>
        </div>
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

<style scoped>
    .action-icon:hover { 
        font-size: 150%;
        cursor: pointer;
    }
</style>