<template>
    <app-modal
        ref="modal"
        title="Select Environment"
    >
        <ul>
            <li
                v-for="environment in environments"
                :key="environment.environmentId"
                class="box action" 
                @click="selectEnvironment(environment)"
            >
                {{ environment.name }}
            </li>
        </ul>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {EnvironmentService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "EnvironmentPickerModal",
    components: {
        "app-modal": Modal
    },
    data: function() {
        return {
            environments: []
        }
    },
    created: function () {
        this.fetchEnvironments();

        system.events.$on("environmentModal:show", function () {
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("environmentModal:hide", function () {
            this.close();
        }.bind(this));
    },
    methods: {
        fetchEnvironments: function () {
            EnvironmentService.getAll().then(function (data) {
                this.environments = data;
            }.bind(this),
            function (error) {
                console.log("Error fetching environments: " + error)
            });
        },
        close: function () {
            this.$refs.modal.close();
        },
        selectEnvironment: function (environment) {
            this.$emit("environment:selected", environment);
            this.close();
        }
    }
}
</script>

<style scoped>
    .box.action:hover {
        background-color: rgba(299, 40, 69, 0.7);
        cursor: pointer
    }
</style>
