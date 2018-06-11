<template>
    <div>        
        <div class="box">
            <p class="subtitle">Environments
                <span class="is-pulled-right">
                    <i class="fas fa-plus action-icon"></i>
                </span>
            </p>
            <ul>
                <li class="box" v-for="environment in environments">
                    {{environment.name}}
                    <span class="is-pulled-right">
                        <i class="fas fa-trash action-icon"></i>
                    </span>
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    import { ApplicationEnvironmentService } from "services/ApplicationProxy.js"

    export default {
        name: "application-environment-list",
        data: function() {
            return {
                environments: []
            }
        },
        props: {
            applicationId: {
                type: Number,
                required: true
            }
        },
        methods: {
            fetchEnvironments: function () {
                ApplicationEnvironmentService.get(this.applicationId).then(function (data) {
                    this.environments = data;
                }.bind(this),
                function (error) {
                    console.log("Error fetching environments for application: " + error)
                });

            },         
            clear: function () {
                this.environments = [];
            }
        },
        created: function () {
            this.fetchEnvironments();
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform:scale(1.5);
        cursor: pointer;
    }
</style>