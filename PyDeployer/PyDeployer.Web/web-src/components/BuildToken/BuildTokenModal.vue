<template>
    <app-modal ref="modal" :title="title">
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Token name" v-model="name" />
            </div>
        </div>
        <div class="field">
            <label class="label">Value</label>
            <div class="control">
                <input class="input" type="text" placeholder="Token value" v-model="value" />
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="save">Save</button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {BuildTokenService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "build-token-modal",
    data: function () {
        return {
            title: "Create Build Token",  
            buildTokenId: -1,
            name: "",
            value: ""
        }
    },
    methods: {
        fetchToken: function () {
            return BuildTokenService.get(this.buildTokenId).then(function (data) {
                this.update(data);
                return true;
            }.bind(this), function (error) {
                console.log("Error fetching build token: " + error)
                return false;
            });
        },
        save: function () {
            let func = null;
            let created = false;
            if (this.buildTokenId < 0) {
                func = BuildTokenService.create
                created = true;
            }
            else {
                func = BuildTokenService.update;
                created = false;
            }
            return func(this.createModel()).then(function (data) {
                let eventName = ""
                if (created) {
                    eventName = "buildTokenModal:created";
                }
                else {
                    eventName = "buildTokenModal:updated";
                }

                system.events.$emit(eventName, data);
                this.close();
                return true;
            }.bind(this), function (error) {
                console.log("Error saving build token: " + error)
                return false;
            });
        },
        close: function () {
            this.$refs.modal.close();
        },
        update: function (data) {
            this.buildTokenId = data.buildTokenId;
            this.name = data.name;
            this.value = data.value
        },
        clear: function () {
            this.buildTokenId = -1;
            this.name = "";
            this.value = "";
        },
        createModel: function () {
            return {
                buildTokenId: this.buildTokenId,
                name: this.name,
                value: this.value
            };
        }
    },
    created: function () {
        system.events.$on("buildTokenModal:create", function () {
            this.clear();
            this.title = "Create Build Token";
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("buildTokenModal:edit", function (applicationToken) {
            this.title = "Edit Build Token";
            this.update(applicationToken);
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("buildTokenModal:hide", function () {
            this.close();
        }.bind(this));
    },
    components: {
        "app-modal": Modal
    }
}
</script>
