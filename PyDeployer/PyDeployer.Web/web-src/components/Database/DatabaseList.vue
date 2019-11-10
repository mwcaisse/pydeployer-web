<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Databases
                <span class="is-pulled-right">
                    <app-icon icon="plus" :action="true" v-on:click.native="create"></app-icon>
                </span>
            </p>
            <ul v-if="databases.length > 0">
                <li class="box" v-for="database in databases">
                    {{database.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="edit" :action="true" v-on:click.native="edit(database)"></app-icon>
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteDatabase(database)"></app-icon>
                    </span>
                </li>
            </ul>
            <p v-else class="has-text-centered">
                No databases exist in this environment.
            </p>
        </div>
        <database-modal :environment-id="environmentId"></database-modal>
    </div>
</template>

<script>
import system from "@app/services/System"
import { DatabaseService } from "@app/services/ApplicationProxy"

import Icon from "@app/components/Common/Icon"
import DatabaseModal, { DatabaseCreatedEvent, DatabaseUpdatedEvent, CreateDatabaseEvent, UpdateDatabaseEvent } from "@app/components/Database/DatabaseModal"

export default {
    name: "database-list",
    data: function () {
        return {
            databases: []
        }
    },
    props: {
        environmentId: {
            type: Number,
            required: true
        }
    },
    methods: {
        fetchDatabases: function () {
            DatabaseService.getForEnvironment(this.environmentId).then(function(databases) {
               this.databases = databases; 
            }.bind(this),
            function (error) {
                console.log("Error fetching databases for environment: " + error);
            });
        },
        clear: function () {
            this.databases = [];
        },
        create: function () {
            system.events.$emit(CreateDatabaseEvent);
        },
        edit: function (database) {
            system.events.$emit(UpdateDatabaseEvent, database);
        },
        deleteDatabase: function (database) {
            return DatabaseService.delete(database.databaseId).then(function () {
                let index = this.tokens.indexOf(database);
                this.tokens.splice(index, 1);
                return true;
            }.bind(this),
            function (error) {
                console.log("Error deleting database: " + error);
                return false;
            });
        }
    },
    created: function () {
        this.fetchDatabases();
        
        system.events.$on(DatabaseCreatedEvent, function (database) {
            this.databases.push(database);
        }.bind(this));
        
        system.events.$on(DatabaseUpdatedEvent, function (database) {
            let index = this.databases.findIndex(function (elm) {
                return elm.databaseId === database.databaseId;
            });
            if (index >= 0) {
                this.databases.splice(index, 1, database);
            }
        }.bind(this));
    },
    components: {
        "app-icon": Icon,
        "database-modal": DatabaseModal
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>