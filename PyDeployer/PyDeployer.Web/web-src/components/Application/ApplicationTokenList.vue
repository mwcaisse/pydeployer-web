<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Application Tokens
                <span class="is-pulled-right">
                    <app-icon icon="fa-plus" :action="true"></app-icon>
                </span>
            </p>
            <ul>
                <li class="box" v-for="token in tokens">
                    {{token.name}}
                    <span class="is-pulled-right">
                        <app-icon icon="fa-edit" :action="true"></app-icon>
                        <app-icon icon="fa-trash" :action="true"></app-icon>
                    </span>
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    import { ApplicationTokenService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"

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
            }
        },
        created: function () {
            this.fetchTokens();
        },
        components: {
            "app-icon": Icon
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>