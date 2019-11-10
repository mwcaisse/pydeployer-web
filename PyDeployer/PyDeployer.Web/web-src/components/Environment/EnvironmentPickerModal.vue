<template>
    <app-modal ref="modal" title="Select Environment">
        <ul>
            <li class="box action" v-for="environment in environments" v-on:click="selectEnvironment(environment)">
                {{environment.name}}
            </li>
        </ul>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import { EnvironmentService } from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "environment-picker-modal",
    data: function() {
        return {
            environments: []
        }
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
    components: {
        "app-modal": Modal
    }
}
</script>

<style scoped>
    .box.action:hover {
        background-color: rgba(299, 40, 69, 0.7);
        cursor: pointer
    }
</style>
