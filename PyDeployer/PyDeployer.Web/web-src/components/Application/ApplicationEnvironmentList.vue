<template>
    <div>        
        <div class="box">
            <p class="subtitle">Environments
                <span class="is-pulled-right">
                    <span  v-on:click="addEnvironment">
                        <i class="fas fa-plus action-icon"></i>
                    </span>                    
                </span>
            </p>
            <ul>
                <li class="box" v-for="environment in environments">
                    {{environment.name}}
                    <span class="is-pulled-right">
                        <span v-on:click="deleteEnvironment(environment)">
                            <i class="fas fa-trash action-icon"></i>
                        </span>                        
                    </span>
                </li>
            </ul>
        </div>
        <environment-modal></environment-modal>
    </div>
</template>

<script>
    import system from "services/System.js"
    import { ApplicationEnvironmentService } from "services/ApplicationProxy.js"
    import EnvironmentModal from "components/Application/EnvironmentModal.vue"

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
            },
            addEnvironment: function () {
                system.events.$emit("environmentModal:show");
            },
            deleteEnvironment: function (environment) {
                ApplicationEnvironmentService.delete(this.applicationId, environment.environmentId).then(function (res) {
                    if (res) {
                        var index = this.environments.indexOf(environment);
                        this.environments.splice(index, 1);
                    }
                    else {
                        console.log("Failed to delete application environment");
                    }
                }.bind(this),
                function (error) {
                    console.log("Error deleting application environment: " + error)
                })
            }
        },
        created: function () {
            this.fetchEnvironments();
        },
        components: {
            EnvironmentModal
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform:scale(1.5);
        cursor: pointer;
    }
</style>