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
                        <input class="input" type="text" v-model="token.value" v-on:blur="saveToken(token)"/>
                    </div>
                </div>
            </div>
        </div>        
    </div>
</template>

<script>    
    import { ApplicationEnvironmentService, ApplicationEnvironmentTokenService } from "@app/services/ApplicationProxy.js" 

    import Icon from "@app/components/Common/Icon.vue"

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
        computed: {
            environmentId: function () {
                if (null !== this.selectedEnvironment && this.selectedEnvironment.environmentId > 0) {
                    return this.selectedEnvironment.environmentId;
                }
                return -1;
            }
        },
        watch: {
            selectedEnvironment: function (val) {
                if (null == val || val.environmentId <= 0) {
                    this.tokens = [];
                }
                else {
                    this.fetchTokens();
                }
            }
        },
        methods: {
            fetchEnvironments: function () {
                ApplicationEnvironmentService.getEnvironmentsForApplication(this.applicationId).then(function (data) {
                    this.environments = data;
                    // Automatically select the first environment
                    if (this.environments.length > 0) {
                        this.selectedEnvironment = this.environments[0];
                    }
                }.bind(this),
                function (error) {
                    console.log("Error fetching environments for application: " + error)
                });
            },
            fetchTokens: function () {
                ApplicationEnvironmentTokenService.getAll(this.applicationId, this.environmentId).then(function (data) {
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
            },
            saveToken: function (token) {
                ApplicationEnvironmentTokenService.save(this.applicationId, this.environmentId, token).then(function (data) {
                    console.log("saved it");
                    console.log(data)
                }.bind(this));
            }
        },
        created: function () {
            this.fetchEnvironments();           
        },
        components: {
            "app-icon": Icon
        }
    }
</script>
