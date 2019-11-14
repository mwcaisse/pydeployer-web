<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Application Tokens
                <span class="is-pulled-right">
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="create"
                    />
                </span>
            </p>
            <ul>
                <li
                    v-for="token in tokens"
                    :key="token.applicationTokenId"
                    class="box"
                >
                    {{ token.name }}
                    <span class="is-pulled-right">
                        <app-icon
                            icon="edit"
                            :action="true"
                            @click.native="edit(token)"
                        />
                        <app-icon
                            icon="trash"
                            :action="true"
                            @click.native="deleteToken(token)"
                        />
                    </span>
                </li>
            </ul>
        </div>
        <application-token-modal :application-id="applicationId" />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import {ApplicationTokenService} from "@app/services/ApplicationProxy.js"
import Icon from "@app/components/Common/Icon.vue"
import ApplicationTokenModal from "@app/components/Application/ApplicationTokenModal.vue"

export default {
    name: "ApplicationTokenList",
    components: {
        "app-icon": Icon,
        "application-token-modal": ApplicationTokenModal
    },
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    data: function() {
        return {
            tokens: []
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
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>