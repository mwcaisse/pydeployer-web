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
                        <i class="fas fa-trash action-icon"></i>
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
                console.log("User clicked on add environments");
                system.events.$emit("environmentModal:show");
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