<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Application Tokens
                <span class="is-pulled-right">
                    <app-icon icon="plus" :action="true" v-on:click.native="create"></app-icon>
                </span>
            </p>
            <ul>
                <li class="box" v-for="token in tokens" :key="token.applicationTokenId">
                    {{token.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="edit" :action="true" v-on:click.native="edit(token)"></app-icon>
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteToken(token)"></app-icon>
                    </span>
                </li>
            </ul>
        </div>
        <application-token-modal :applicationId="applicationId"></application-token-modal>
    </div>
</template>

<script>
import system from "@app/services/System.js"
import {ApplicationTokenService} from "@app/services/ApplicationProxy.js"
import Icon from "@app/components/Common/Icon.vue"
import ApplicationTokenModal from "@app/components/Application/ApplicationTokenModal.vue"

export default {
    name: "application-token-list",
    data: function() {
        return {
            tokens: []
        }
    },
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    methods: {
        fetchTokens: function () {
            ApplicationTokenService.getForApplication(this.applicationId).then(function (data) {
                this.tokens = data;
            }.bind(this),
            function (error) {
                console.log("Error fetching tokens for application: " + error)
            });

        },
        clear: function () {
            this.tokens = [];
        },
        create: function () {
            system.events.$emit("applicationTokenModal:create");
        },
        edit: function (applicationToken) {
            system.events.$emit("applicationTokenModal:edit", applicationToken);
        },
        deleteToken: function (applicationToken) {
            return ApplicationTokenService.delete(this.applicationId, applicationToken.applicationTokenId).then(function (res) {
                if (res) {
                    let index = this.tokens.indexOf(applicationToken);
                    this.tokens.splice(index, 1);
                }
                return res;
            }.bind(this), function (error) {
                console.log("Error deleting application token: " + error);
                return false;
            });

        }
    },
    created: function () {
        this.fetchTokens();           

        system.events.$on("applicationTokenModal:created", function (applicationToken) {
            this.tokens.push(applicationToken);
        }.bind(this));

        system.events.$on("applicationTokenModal:updated", function (applicationToken) {
            let index = this.tokens.findIndex(function (elm) {
                return elm.applicationTokenId === applicationToken.applicationTokenId;
            });
            if (index >= 0) {
                this.tokens.splice(index, 1, applicationToken);
            }
        }.bind(this));
    },
    components: {
        "app-icon": Icon,
        "application-token-modal": ApplicationTokenModal
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>