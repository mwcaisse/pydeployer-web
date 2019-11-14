<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Applications
                <span class="is-pulled-right">
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="addApplication"
                    />
                </span>
            </p>
            <ul v-if="applications.length > 0">
                <li
                    v-for="application in applications"
                    :key="application.applicationId"
                    class="box"
                >
                    {{ application.name }}
                    <span class="is-pulled-right">
                        <app-icon
                            icon="clone"
                            :action="true"
                            @click.native="viewApplication(application)"
                        />
                        <app-icon
                            icon="trash"
                            :action="true"
                            @click.native="deleteApplication(application)"
                        />                     
                    </span>
                </li>
            </ul>
            <p
                v-else
                class="has-text-centered"
            >
                This environment has no applications.
            </p>
        </div>  
        <application-picker-modal @application:selected="applicationSelected" />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import Links from "@app/services/Links.js"
import {ApplicationEnvironmentService} from "@app/services/ApplicationProxy.js"

import Icon from "@app/components/Common/Icon.vue"
import ApplicationPickerModal, {ApplicationPickerShowEvent} from "@app/components/Application/ApplicationPickerModal.vue";

export default {
    name: "ApplicationList",
    components: {
        "app-icon": Icon,
        "application-picker-modal": ApplicationPickerModal
    },
    props: {
        environmentId: {
            type: Number,
            required: true
        }            
    },
    data: function() {
        return {
            applications: []
        }
    },        
    created: function () {
        this.fetchApplications()
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
    }
}
</script>


<style scoped>

</style>