<template>
    <app-modal ref="modal" title="Create Environment">
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Environment name" v-model="name" />
            </div>
        </div>
        <div class="field">
            <label class="label">Host Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Host name" v-model="hostName" />
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="save">Save</button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {EnvironmentService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "environment-modal",
    data: function () {
        return {
            title: "Create Environment",
            environmentId: -1,
            name: "",
            hostName: ""
        }
    },
    methods: {
        fetchEnvironment: function () {
            return EnvironmentService.get(this.environmentId).then(function (data) {
                this.update(data);
                return true;
            }.bind(this), function (error) {
                console.log("Error fetching environment: " + error)
                return false;
            });
        },
        save: function () {
            var func = null;
            var created = false;
            if (this.environmentId < 0) {
                func = EnvironmentService.create;
                created = true;
            }
            else {
                func = EnvironmentService.update;
                created = false;
            }
            return func(this.createModel()).then(function (data) {
                var eventName = ""
                if (created) {
                    eventName = "environmentModal:created";
                }
                else {
                    eventName = "environmentModal:updated";
                }

                system.events.$emit(eventName, data);
                this.close();
                return true;
            }.bind(this), function (error) {
                console.log("Error saving environment: " + error)
                return false;
            });
        },
        close: function () {
            this.$refs.modal.close();
        },
        update: function (data) {
            this.environmentId = data.environmentId;
            this.name = data.name;
            this.hostName = data.hostName;
        },
        clear: function () {
            this.environmentId = -1;
            this.name = "";
            this.hostName = "";
        },
        createModel: function () {
            return {
                environmentId: this.environmentId,
                name: this.name,
                hostName: this.hostName
            };
        }
    },
    created: function () {
        system.events.$on("environmentModal:create", function () {
            this.clear();
            this.title = "Create Environment";
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("environmentModal:edit", function (environment) {
            this.title = "Edit Environment";
            this.update(environment);
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
