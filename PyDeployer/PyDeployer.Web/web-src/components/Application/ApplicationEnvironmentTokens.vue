<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Token Values          
            </p>      
            <div class="field has-addons is-horizontal">                    
                <label class="label">Environment</label>&nbsp;
                <p
                    v-for="environment in environments"
                    :key="environment.environmentId"
                    class="control"
                >
                    <button
                        type="button"
                        class="button" 
                        :class="{'is-danger': environment == selectedEnvironment }"
                        @click="selectEnvironment(environment)"
                    >
                        {{ environment.name }}
                    </button>
                </p>
            </div>
            <div>
                <div
                    v-for="token in tokens"
                    :key="token.applicationEnvironmentTokenId"
                    class="field"
                >
                    <label class="label">{{ token.name }}</label>
                    <div class="control">
                        <input
                            v-model="token.value"
                            class="input"
                            type="text"
                            @blur="saveToken(token)"
                        >
                    </div>
                </div>
            </div>
        </div>        
    </div>
</template>

<script>    
import {ApplicationEnvironmentService, ApplicationEnvironmentTokenService} from "@app/services/ApplicationProxy.js" 

export default {
    name: "ApplicationEnvironmentTokens",
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    data: function() {
        return {
            environments: [],
            tokens: [],
            selectedEnvironment: null
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
    created: function () {
        this.fetchEnvironments();           
    },
    methods: {
        fetchEnvironments: function () {
            ApplicationEnvironmentService.getEnvironmentsForApplication(this.applicationId).then(function (data) {
                this.environments = data;
                //Automatically select the first environment
                if (this.environments.length > 0) {
                    [this.selectedEnvironment] = this.environments;
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
            });
        }
    }
}
</script>
