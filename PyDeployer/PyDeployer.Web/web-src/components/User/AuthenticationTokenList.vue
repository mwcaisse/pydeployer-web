<template>
    <div>           
        <ul>
            <li class="box" v-for="token in tokens">
                <div class="columns is-vcentered">
                    <div class="column is-size-2">
                        <app-icon icon="key" :action="false"></app-icon>
                    </div>
                    <div class="column is-four-fifths">
                        <p class="is-size-5 has-text-weight-bold">{{token.description}}</p>
                        <p class="is-size-6" v-if="token.lastLogin">Last used on {{ token.lastLogin | formatDateTime }} from {{ token.lastLoginAddress }}</p>
                        <p class="is-size-6" v-else>This token has never been used</p>
                        <p class="is-size-7">Added on {{ token.createDate | formatDate }}</p>
                        <p class="is-size-7" v-if="token.expirationDate">Expires on {{ token.expirationDate | formatDateTime }}</p>
                    </div>
                    <div class="column">       
                        <span class="is-pulled-right is-size-5">
                            <app-icon icon="trash" :action="true"></app-icon>
                        </span>
                    </div>
                </div>
            </li>
        </ul>          
    </div>
</template>

<script>
    import system from "services/System.js"
    import { UserAuthenticationTokenService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"


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

            }
        },
        components: {
            "app-icon": Icon
        },
        created: function () {
            this.fetchTokens();
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>