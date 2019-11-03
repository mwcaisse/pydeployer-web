<template>
    <app-modal ref="modal" title="Create Application">
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Application name" v-model="name" />
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="save">Save</button>
        </template>
    </app-modal>
</template>

<script>
    import system from "@app/services/System.js"
    import { ApplicationService } from "@app/services/ApplicationProxy.js"

    import Modal from "@app/components/Common/Modal.vue"

    export default {
        name: "application-modal",
        data: function () {
            return {
                title: "Create Application",         
                applicationId: -1,
                name: ""
            }
        },
        methods: {
            fetchApplication: function () {
                return ApplicationService.get(this.applicationId).then(function (data) {
                    this.update(data);
                    return true;
                }.bind(this), function (error) {
                    console.log("Error fetching application: " + error)
                    return false;
                });
            },
            save: function () {
                var func;
                var created = false;
                if (this.applicationId < 0) {
                    func = ApplicationService.create
                    created = true;
                }
                else {
                    func = ApplicationService.update;
                    created = false;
                }
                return func(this.createModel()).then(function (data) {
                    var eventName = ""
                    if (created) {
                        eventName = "applicationModal:created";
                    }
                    else {
                        eventName = "applicationModal:updated";
                    }

                    system.events.$emit(eventName, data);
                    this.close();
                    return true;
                }.bind(this), function (error) {
                    console.log("Error saving application: " + error)
                    return false;
                });
            },
            close: function () {
                this.$refs.modal.close();
            },
            update: function (data) {
                this.applicationId = data.applicationId;
                this.name = data.name;
            },
            clear: function () {
                this.applicationId = -1;
                this.name = "";
            },
            createModel: function () { 
                return {                 
                    applicationId: this.applicationId,
                    name: this.name
                };
            }
        },
        created: function () {
            system.events.$on("applicationModal:create", function () {
                this.clear();
                this.title = "Create Application";
                this.$refs.modal.open();
            }.bind(this));

            system.events.$on("applicationModal:edit", function (application) {
                this.title = "Edit Application";
                this.update(application);
                this.$refs.modal.open();
            }.bind(this));

            system.events.$on("applicationModal:hide", function () {
                this.close();
            }.bind(this));
        },
        components: {
            "app-modal": Modal
        }
    }
</script>
