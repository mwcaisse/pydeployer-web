<template>
    <div>
        <section class="section">
            <div class="container">
                <div class="title">
                    <span>Authentication Tokens</span>
                    <span class="is-pulled-right has-text-left is-size-3">
                        <app-icon icon="plus" :action="true" v-on:click.native="create"></app-icon>
                    </span>
                </div>                
            </div>
        </section>
        <ul>
            <li class="box" v-for="token in tokens">
                <div class="columns is-vcentered is-mobile">
                    <div class="column is-size-2">
                        <app-icon icon="key" :action="false"></app-icon>
                    </div>
                    <div class="column is-four-fifths-tablet is-two-thirds-mobile">
                        <p class="is-size-5 has-text-weight-bold">{{token.description}}</p>
                        <p class="is-size-6" v-if="token.lastLogin">Last used on <span class="has-text-weight-bold">{{ token.lastLogin | formatDateTime }}</span> from <span class="has-text-weight-bold">{{ token.lastLoginAddress }}</span></p>
                        <p class="is-size-6" v-else>This token has never been used</p>
                        <p class="is-size-7">Added on {{ token.createDate | formatDate }}</p>
                        <p class="is-size-7" v-if="token.expirationDate">Expires on {{ token.expirationDate | formatDateTime }}</p>
                    </div>
                    <div class="column">
                        <span class="is-pulled-right is-size-5">
                            <app-icon icon="trash" :action="true" v-on:click.native="deleteToken(token)"></app-icon>
                        </span>
                    </div>
                </div>
            </li>
        </ul>
        <authentication-token-modal></authentication-token-modal>
    </div>
</template>

<script>
import system from "@app/services/System.js"
import { UserAuthenticationTokenService } from "@app/services/ApplicationProxy.js"
import AuthenticationTokenModal from "@app/components/User/AuthenticationTokenModal.vue"
import Icon from "@app/components/Common/Icon.vue"


export default {
    name: "authentication-token-list",
    data: function() {
        return {
            tokens: []
        }
    },  
    methods: {
        fetchTokens: function () {
            UserAuthenticationTokenService.getActive().then(function (data) {
                this.tokens = data.data; // returns a paged object, so tokens are in data.data
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
        },
    },
    components: {
        "app-icon": Icon,
        "authentication-token-modal": AuthenticationTokenModal
    },
    created: function () {
        this.fetchTokens();

        system.events.$on("authenticationTokenModal:created", function () {
            this.fetchTokens();
        }.bind(this));   
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>