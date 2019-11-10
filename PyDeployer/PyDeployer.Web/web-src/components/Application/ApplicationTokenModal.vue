<template>
    <app-modal ref="modal" :title="title">
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Token name" v-model="name" />
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="save">Save</button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import { ApplicationTokenService } from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "application-token-modal",
    data: function () {
        return {
            title: "Create Application Token",
            applicationTokenId: -1,
            name: ""
        }
    },
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    methods: {
        fetchToken: function () {
            return ApplicationTokenService.get(this.applicationId,
                this.applicationTokenId).then(function (data) {
                    this.update(data);
                    return true;
                }.bind(this), function (error) {
                    console.log("Error fetching application token: " + error)
                    return false;
                });
        },
        save: function () {
            var func;
            var created = false;
            if (this.applicationTokenId < 0) {
                func = ApplicationTokenService.create
                created = true;
            }
            else {
                func = ApplicationTokenService.update;
                created = false;
            }
            return func(this.applicationId, this.createModel()).then(function (data) {
                var eventName = ""
                if (created) {
                    eventName = "applicationTokenModal:created";
                }
                else {
                    eventName = "applicationTokenModal:updated";
                }

                system.events.$emit(eventName, data);
                this.close();
                return true;
            }.bind(this), function (error) {
                console.log("Error saving application token: " + error)
                return false;
            });
        },
        close: function () {
            this.$refs.modal.close();
        },
        update: function (data) {
            this.applicationTokenId = data.applicationTokenId;
            this.name = data.name;
        },
        clear: function () {
            this.applicationTokenId = -1;
            this.name = "";
        },
        createModel: function () {
            return {
                applicationTokenId: this.applicationTokenId,
                applicationId: this.applicationId,
                name: this.name
            };
        }
    },
    created: function () {
        system.events.$on("applicationTokenModal:create", function () {
            this.clear();
            this.title = "Create Application Token";
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("applicationTokenModal:edit", function (applicationToken) {
            this.title = "Edit Application Token";
            this.update(applicationToken);
            this.$refs.modal.open();
        }.bind(this));

        system.events.$on("applicationTokenModal:hide", function () {
            this.close();
        }.bind(this));
    },
    components: {
        "app-modal": Modal
    }
}
</script>
