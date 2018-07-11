<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Token Values          
            </p>      
            <div class="field has-addons is-horizontal">                    
                <label class="label">Environment</label>&nbsp;
                <p class="control" v-for="environment in environments">
                    <button type="button" class="button" 
                            v-bind:class="{'is-danger': environment == selectedEnvironment }"
                            v-on:click="selectEnvironment(environment)">
                    {{ environment.name }}</button>
                </p>
            </div>
            <div>
                <div class="field" v-for="token in tokens">
                    <label class="label">{{ token.name }}</label>
                    <div class="control">
                        <input class="input" type="text"/>
                    </div>
                </div>
            </div>
        </div>        
    </div>
</template>

<script>
    import system from "services/System.js"
    import { ApplicationEnvironmentService, ApplicationTokenService } from "services/ApplicationProxy.js" 

    import Icon from "components/Common/Icon.vue"

    export default {
        name: "application-environment-tokens",
        data: function() {
            return {
                environments: [],
                tokens: [],
                selectedEnvironment: null
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
                    // If there is only one environment, then automatically select it
                    if (this.environments.length == 1) {
                        this.selectedEnvironment = this.environments[0];
                    }
                }.bind(this),
                function (error) {
                    console.log("Error fetching environments for application: " + error)
                });

            },
            fetchTokens: function () {
                ApplicationTokenService.getForApplication(this.applicationId).then(function (data) {
                    this.tokens = data;
                }.bind(this),
                function (error) {
                    console.log("Error fetching tokens for application: " + error)
                });
            },
            clear: function () {
                this.environments = [];
            },   
            selectEnvironment: function (environment) {
                this.selectedEnvironment = environment;
            }
        },
        created: function () {
            this.fetchEnvironments();
            this.fetchTokens();
        },
        components: {
            "app-icon": Icon
        }
    }
</script>
