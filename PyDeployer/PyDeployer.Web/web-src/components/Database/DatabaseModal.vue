<template>
    <app-modal
        ref="modal"
        :title="title"
    >
        <div class="field">
            <label class="label">Name</label>
            <div class="control">
                <input
                    v-model="name"
                    class="input"
                    type="text"
                    placeholder="Database name"
                >
            </div>
        </div>
        <div class="field">
            <label class="label">Host</label>
            <div class="control">
                <input
                    v-model="host"
                    class="input"
                    type="text"
                    placeholder="Hostname of the database"
                >
            </div>
        </div>
        <div class="field">
            <label class="label">Port</label>
            <div class="control">
                <input
                    v-model="port"
                    class="input"
                    type="text"
                    placeholder="Port database listens on"
                >
            </div>
        </div>
        <div class="field">
            <label class="label">User</label>
            <div class="control">
                <input
                    v-model="user"
                    class="input"
                    type="text"
                    placeholder="Maintenance username"
                >
            </div>
        </div>
        <div class="field">
            <label class="label">Password</label>
            <div class="control">
                <input
                    v-model="password"
                    class="input"
                    type="text"
                    placeholder="Maintenance user's password"
                >
            </div>
        </div>
        <template slot="footer-buttons">
            <button
                class="button"
                type="button"
                @click="save"
            >
                Save
            </button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System"
import {DatabaseService} from "@app/services/ApplicationProxy"

import Modal from "@app/components/Common/Modal"

const databaseCreatedEvent = "databaseModal:created";
const databaseUpdatedEvent = "databaseModal:updated";
const createDatabaseEvent = "databaseModal:create";
const updateDatabaseEvent = "databaseModal:update";    
const hideModalEvent = "databaseModal:hide";

export default {
    name: "DatabaseModal",
    components: {
        "app-modal": Modal
    },
    props: {
        environmentId: {
            type: Number,
            required: true
        }
    },
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
    methods: {
        save: function () {
            let func = null;
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
            this.password = data.password;
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
                host: this.host,
                port: this.port,
                user: this.user,
                password: this.password
            };
        }
        
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