<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Environments
                <span class="is-pulled-right">
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="create"
                    />
                </span>
            </p>
            <ul v-if="environments.length > 0">
                <li
                    v-for="environment in environments"
                    :key="environment.environmentId"
                    class="box"
                >
                    {{ environment.name }}
                    <span class="is-pulled-right">
                        <app-icon
                            icon="clone"
                            :action="true"
                            @click.native="viewEnvironment(environment)"
                        />
                        <app-icon
                            icon="trash"
                            :action="true"
                            @click.native="deleteEnvironment(environment)"
                        />
                    </span>
                </li>
            </ul>
            <p
                v-else
                class="has-text-centered"
            >
                No environments exist yet!
            </p>
        </div> 
        <environment-modal />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import Links from "@app/services/Links.js"
import {EnvironmentService} from "@app/services/ApplicationProxy.js"
import Icon from "@app/components/Common/Icon.vue"
import EnvironmentModal from "@app/components/Environment/EnvironmentModal.vue"

export default {
    name: "EnvironmentList",
    components: {
        "app-icon": Icon,
        "environment-modal": EnvironmentModal
    },
    data: function() {
        return {
            environments: []
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
    }
}
</script>
