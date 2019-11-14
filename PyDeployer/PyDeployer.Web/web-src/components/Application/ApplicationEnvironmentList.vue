<template>
    <div>        
        <div class="box">
            <p class="subtitle">
                Environments
                <span class="is-pulled-right">                   
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="addEnvironment"
                    />
                </span>
            </p>
            <ul>
                <li
                    v-for="environment in environments"
                    :key="environment.applicationEnvironmentId"
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
        </div>
        <environment-picker-modal @environment:selected="environmentSelected" />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import Links from "@app/services/Links.js"
import {ApplicationEnvironmentService} from "@app/services/ApplicationProxy.js"
import EnvironmentPickerModal from "@app/components/Environment/EnvironmentPickerModal.vue"    

import Icon from "@app/components/Common/Icon.vue"

export default {
    name: "ApplicationEnvironmentList",
    components: {
        "environment-picker-modal": EnvironmentPickerModal,
        "app-icon": Icon
    },
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    data: function() {
        return {
            environments: []
        }
    },
    created: function () {
        this.fetchEnvironments();
    },
    methods: {
        fetchEnvironments: function () {
            ApplicationEnvironmentService.getEnvironmentsForApplication(this.applicationId).then(function (data) {
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
                    let index = this.environments.indexOf(environment);
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
        viewEnvironment: function (environment) {
            window.location = Links.environment(environment.environmentId);
        },
        environmentSelected: function (environment) {
            ApplicationEnvironmentService.create(this.applicationId, environment.environmentId).then(function () {
                this.environments.push(environment);
            }.bind(this),
            function (error) {
                console.log("Error associating environment with application: " + error)
            });
        }
    }
}
</script>
