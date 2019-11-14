<template>
    <div>
        <section class="section">
            <div class="container">
                <div class="title">
                    <span>Authentication Tokens</span>
                    <span class="is-pulled-right has-text-left is-size-3">
                        <app-icon
                            icon="plus"
                            :action="true"
                            @click.native="create"
                        />
                    </span>
                </div>                
            </div>
        </section>
        <ul>
            <li
                v-for="token in tokens"
                :key="token.userAuthenticationTokenId"
                class="box"
            >
                <div class="columns is-vcentered is-mobile">
                    <div class="column is-size-2">
                        <app-icon
                            icon="key"
                            :action="false"
                        />
                    </div>
                    <div class="column is-four-fifths-tablet is-two-thirds-mobile">
                        <p class="is-size-5 has-text-weight-bold">
                            {{ token.description }}
                        </p>
                        <p
                            v-if="token.lastLogin"
                            class="is-size-6"
                        >
                            Last used on <span class="has-text-weight-bold">{{ token.lastLogin | formatDateTime }}</span> from <span class="has-text-weight-bold">{{ token.lastLoginAddress }}</span>
                        </p>
                        <p
                            v-else
                            class="is-size-6"
                        >
                            This token has never been used
                        </p>
                        <p class="is-size-7">
                            Added on {{ token.createDate | formatDate }}
                        </p>
                        <p
                            v-if="token.expirationDate"
                            class="is-size-7"
                        >
                            Expires on {{ token.expirationDate | formatDateTime }}
                        </p>
                    </div>
                    <div class="column">
                        <span class="is-pulled-right is-size-5">
                            <app-icon
                                icon="trash"
                                :action="true"
                                @click.native="deleteToken(token)"
                            />
                        </span>
                    </div>
                </div>
            </li>
        </ul>
        <authentication-token-modal />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import {UserAuthenticationTokenService} from "@app/services/ApplicationProxy.js"
import AuthenticationTokenModal from "@app/components/User/AuthenticationTokenModal.vue"
import Icon from "@app/components/Common/Icon.vue"


export default {
    name: "AuthenticationTokenList",
    components: {
        "app-icon": Icon,
        "authentication-token-modal": AuthenticationTokenModal
    },
    data: function() {
        return {
            tokens: []
        }
    },
    created: function () {
        this.fetchTokens();

        system.events.$on("authenticationTokenModal:created", function () {
            this.fetchTokens();
        }.bind(this));   
    },  
    methods: {
        fetchTokens: function () {
            UserAuthenticationTokenService.getActive().then(function (data) {
                //Returns a paged object, so tokens are in data.data
                this.tokens = data.data; 
            }.bind(this),
            function (error) {
                console.log("Error fetching tokens for application: " + error)
            });
        },
        create: function () {
            system.events.$emit("authenticationTokenModal:create");
        },
        deleteToken: function (token) {
            return UserAuthenticationTokenService.delete(token.userAuthenticationTokenId).then(function (res) {
                if (res) {
                    var index = this.tokens.indexOf(token);
                    this.tokens.splice(index, 1);
                }
                return res;
            }.bind(this), function (error) {
                console.log("Error deleting authentication token: " + error);
                return false;
            })
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