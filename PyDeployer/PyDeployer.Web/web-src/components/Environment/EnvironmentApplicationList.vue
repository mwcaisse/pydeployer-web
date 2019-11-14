<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Applications
                <span class="is-pulled-right">
                    <app-icon icon="plus" :action="true" v-on:click.native="addApplication"></app-icon>
                </span>
            </p>
            <ul v-if="applications.length > 0">
                <li class="box" v-for="application in applications" :key="application.applicationId">
                    {{application.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="clone" :action="true" v-on:click.native="viewApplication(application)"></app-icon>
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteApplication(application)"></app-icon>                     
                    </span>
                </li>
            </ul>
            <p v-else class="has-text-centered">
                This environment has no applications.
            </p>
        </div>  
        <application-picker-modal v-on:application:selected="applicationSelected"></application-picker-modal>
    </div>
</template>

<script>
import system from "@app/services/System.js"
import Links from "@app/services/Links.js"
import {ApplicationEnvironmentService} from "@app/services/ApplicationProxy.js"

import Icon from "@app/components/Common/Icon.vue"
import ApplicationPickerModal, {ApplicationPickerShowEvent} from "@app/components/Application/ApplicationPickerModal.vue";

export default {
    name: "application-list",
    data: function() {
        return {
            applications: []
        }
    },
    props: {
        environmentId: {
            type: Number,
            required: true
        }            
    },        
    methods: {
        fetchApplications: function () {
            ApplicationEnvironmentService.getApplicationsForEnvironment(this.environmentId).then(function (data) {
                this.applications = data;
            }.bind(this),
            function (error) {
                console.log("Error fetching applications: " + error)
            });
        },
        addApplication: function () {
            system.events.$emit(ApplicationPickerShowEvent);
        },
        deleteApplication: function (application) {
            ApplicationEnvironmentService.delete(application.applicationId, this.environmentId).then(function (res) {
                if (res) {
                    let index = this.applications.indexOf(application);
                    this.applications.splice(index, 1);
                }
                else {
                    console.log("Failed to delete application.");
                }
            }.bind(this), function (error) {
                console.log("Error deleting application: " + error)
            })
        },
        viewApplication: function (application) {
            window.location = Links.application(application.applicationId);
        },
        applicationSelected: function (application) {
            ApplicationEnvironmentService.create(application.applicationId, this.environmentId).then(function () {
                this.applications.push(application);
            }.bind(this),
            function (error) {
                console.log("Error associating application with environment: "+ error);
            });
        }
    },        
    created: function () {
        this.fetchApplications()
    },
    components: {
        "app-icon": Icon,
        "application-picker-modal": ApplicationPickerModal
    }
}
</script>


<style scoped>

</style>