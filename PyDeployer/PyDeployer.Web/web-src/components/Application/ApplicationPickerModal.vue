<template>
    <app-modal
        ref="modal"
        title="Select Application"
    >
        <ul>
            <li
                v-for="application in applications"
                :key="application.applicationId"
                class="box action"
                @click="selectApplication(application)"
            >
                {{ application.name }}
            </li>
        </ul>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {ApplicationService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

const applicationSelectedEvent = "application:selected";
const modalShowEvent = "applicationPickerModal:show";
const modalHideEvent = "applicationPickerModal:hide";

export default {
    name: "ApplicationPickerModal",
    components: {
        "app-modal": Modal
    },
    data: function () {
        return {
            applications: []
        }
    },
    created: function () {
        this.fetchApplications();
        
        system.events.$on(modalShowEvent, function () {
            this.$refs.modal.open();
        }.bind(this));
        
        system.events.$on(modalHideEvent, function () {
            this.close();
        }.bind(this));
        
    },
    methods: {
        fetchApplications: function () {
            ApplicationService.getAll().then(function (data) {
                this.applications = data;
            }.bind(this),
            function (error) {
                console.log("Error fetching applications: " + error);
            });
        },
        close: function () {
            this.$refs.modal.close();
        },
        selectApplication: function (application) {
            this.$emit(applicationSelectedEvent, application);
            this.close();
        }
        
    }
};

export {
    applicationSelectedEvent as ApplicationSelectedEvent,
    modalShowEvent as ApplicationPickerShowEvent,
    modalHideEvent as ApplicationPickerHideEvent
};
</script>

<style scoped>
    .box.action:hover {
        background-color: rgba(299, 40, 69, 0.7);
        cursor: pointer
    }
</style>
