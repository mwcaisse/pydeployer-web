<template>   

    <div>
        <div class="box">
            <p class="subtitle">
                Environments
                <span class="is-pulled-right">
                    <app-icon icon="plus" :action="true" v-on:click.native="create"></app-icon>
                </span>
            </p>
            <ul v-if="environments.length > 0">
                <li class="box" v-for="environment in environments">
                    {{environment.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="clone" :action="true" v-on:click.native="viewEnvironment(environment)"></app-icon>
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteEnvironment(environment)"></app-icon>
                    </span>
                </li>
            </ul>
            <p v-else class="has-text-centered">
                No environments exist yet!
            </p>
        </div> 
        <environment-modal></environment-modal>
    </div>
</template>

<script>
    import system from "@app/services/System.js"
    import Links from "@app/services/Links.js"
    import { EnvironmentService } from "@app/services/ApplicationProxy.js"
    import Icon from "@app/components/Common/Icon.vue"
    import EnvironmentModal from "@app/components/Environment/EnvironmentModal.vue"

    export default {
        name: "environment-list",
        data: function() {
            return {
                environments: []
            }
        },
        methods: {
            fetchEnvironments: function () {
                EnvironmentService.getAll().then(function (data) {
                    this.environments = data;
                }.bind(this), function (error) {
                    console.log("Error fetching environments: " + error)
                });
            },
            create: function () {
                system.events.$emit("environmentModal:create");
            },
            deleteEnvironment: function (environment) {
                EnvironmentService.delete(environment.environmentId).then(function (res) {
                    if (res) {
                        var index = this.environments.indexOf(environment);
                        this.environments.splice(index, 1);
                    }
                    else {
                        console.log("Failed to delete environment.");
                    }
                }.bind(this), function (error) {
                    console.log("Error deleting environment: " + error)
                })
            },
            viewEnvironment: function (environment) {
                window.location = Links.environment(environment.environmentId);
            }
        },
        created: function () {
            this.fetchEnvironments();

            system.events.$on("environmentModal:created", function (environment) {
                this.environments.push(environment);
            }.bind(this));

            system.events.$on("environmentModal:updated", function (environment) {
                var index = this.environments.findIndex(function (elm) {
                    return elm.environmentId == environment.environmentId;
                });
                if (index >= 0) {
                    this.environments.splice(index, 1, environment);
                }
            }.bind(this));
        },
        components: {
            "app-icon": Icon,
            "environment-modal": EnvironmentModal
        }
    }
</script>
