<template>
    <div>        
        <div class="box">
            <p class="subtitle">Environments
                <span class="is-pulled-right">                   
                    <app-icon icon="plus" :action="true" v-on:click.native="addEnvironment"/>
                </span>
            </p>
            <ul>
                <li class="box" v-for="environment in environments">
                    {{environment.name}}
                    <span class="is-pulled-right">                           
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteEnvironment(environment)"></app-icon>                
                    </span>
                </li>
            </ul>
        </div>
        <environment-picker-modal v-on:environment:selected="environmentSelected"></environment-picker-modal>
    </div>
</template>

<script>
    import system from "services/System.js"
    import { ApplicationEnvironmentService } from "services/ApplicationProxy.js"
    import EnvironmentPickerModal from "components/Environment/EnvironmentPickerModal.vue"

    import Icon from "components/Common/Icon.vue"

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
            },
            environmentSelected: function (environment) {
                ApplicationEnvironmentService.create(this.applicationId, environment.environmentId).then(function (res) {
                    this.environments.push(environment);
                }.bind(this),
                function (error) {
                    console.log("Error associating environment with application: " + error)
                });
            }
        },
        created: function () {
            this.fetchEnvironments();
        },
        components: {
            "environment-picker-modal": EnvironmentPickerModal,
            "app-icon": Icon
        }
    }
</script>
