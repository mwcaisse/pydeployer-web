<template>
    <app-modal ref="modal" :title="title">
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input class="input" type="text" placeholder="Database name" v-model="name">
            </div>
        </div>
        <div class="field">
            <label class="label">Host</label>
            <div class="control">
                <input class="input" type="text" placeholder="Hostname of the database" v-model="host">
            </div>
        </div>
        <div class="field">
            <label class="label">Port</label>
            <div class="control">
                <input class="input" type="text" placeholder="Port database listens on" v-model="port">
            </div>
        </div>
        <div class="field">
            <label class="label">User</label>
            <div class="control">
                <input class="input" type="text" placeholder="Maintenance username" v-model="user">
            </div>
        </div>
        <div class="field">
            <label class="label">Password</label>
            <div class="control">
                <input class="input" type="text" placeholder="Maintenance user's password" v-model="password">
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="save">Save</button>
        </template>
    </app-modal>
</template>

<script>
    import system from "@app/services/System"
    import { DatabaseService } from "@app/services/ApplicationProxy"
    
    import Modal from "@app/components/Common/Modal"
    
    const databaseCreatedEvent = "databaseModal:created";
    const databaseUpdatedEvent = "databaseModal:updated";
    const createDatabaseEvent = "databaseModal:create";
    const updateDatabaseEvent = "databaseModal:update";    
    const hideModalEvent = "databaseModal:hide";
    
    export default {
        name: "database-modal",
        data: function () {
            return {
                title: "Create Database",
                databaseId: -1,
                name: "",
                type: "",
                host: "",
                port: "",
                user: "",
                password: ""                
            }
        },
        props: {
            environmentId: {
                type: Number,
                required: true
            }
        },
        methods: {
            save: function () {
                let func;
                let eventName = "";
                if (this.databaseId < 0) {
                    func = function (database) {
                        return DatabaseService.create(this.environmentId, database);
                    }.bind(this);
                    eventName = databaseCreatedEvent;
                }
                else {
                    func = function (database) {
                        return DatabaseService.update(this.databaseId, database);
                    }.bind(this);
                    eventName = databaseUpdatedEvent;
                }
                
                return func(this.createModel()).then(function (data) {
                    system.events.$emit(eventName, data);
                    this.close();
                    return true;
                }.bind(this), function (error) {
                    console.log("Error saving database: " + error);    
                });
            },
            close: function () {
                this.$refs.modal.close();
            },
            update: function (data) {
                this.databaseId = data.databaseId;
                this.name = data.name;
                this.type = data.type;
                this.host = data.host;
                this.port = data.port;
                this.user = data.user;
                this.password =  data.password;
            },
            clear: function () {
                this.databaseId = -1;
                this.name = "";
                this.type = "";
                this.host = "";
                this.port = "";
                this.user = "";
                this.password = "";
            },
            createModel: function () {
                return {
                    databaseId: this.databaseId,
                    name: this.name,
                    //type: this.type,
                    host: this.host,
                    port: this.port,
                    user: this.user,
                    password: this.password
                };
            }
            
        },
        created: function () {
            system.events.$on(createDatabaseEvent, function () {
                this.clear();
                this.title = "Create Database";
                this.$refs.modal.open();
            }.bind(this));
            
            system.events.$on(updateDatabaseEvent, function (database) {
                this.title = "Edit Database";
                this.update(database);
                this.$refs.modal.open();
            }.bind(this));
            
            system.events.$on(hideModalEvent, function () {
                this.close();
            }.bind(this));
        },
        components: {
            "app-modal": Modal
        }
    }
    
    export {
        databaseCreatedEvent as DatabaseCreatedEvent,
        databaseUpdatedEvent as DatabaseUpdatedEvent,
        createDatabaseEvent as CreateDatabaseEvent,
        updateDatabaseEvent as UpdateDatabaseEvent,
        hideModalEvent as HideModalEvent
    }
</script>

<style scoped>

</style>