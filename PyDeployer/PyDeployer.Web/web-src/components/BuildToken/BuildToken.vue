<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Build Tokens
                <span class="is-pulled-right">
                    <app-icon
                        icon="plus"
                        :action="true"
                        @click.native="create"
                    />
                </span>
            </p>
            <ul v-if="tokens.length > 0">
                <li
                    v-for="token in tokens"
                    :key="token.buildTokenId"
                    class="box"
                >
                    <div>
                        <span class="is-size-5 is-bold">{{ token.name }}</span>
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
                    </div>
                    <div>
                        {{ token.value }}
                    </div>
                </li>
            </ul>
            <p
                v-else
                class="has-text-centered"
            >
                No build tokens exist yet!
            </p>
        </div>  
        <build-token-modal />
    </div>
</template>

<script>
import system from "@app/services/System.js"
import {BuildTokenService} from "@app/services/ApplicationProxy.js"
import Icon from "@app/components/Common/Icon.vue"
import BuildTokenModal from "@app/components/BuildToken/BuildTokenModal.vue"


export default {
    name: "BuildTokenList",
    components: {
        "app-icon": Icon,
        "build-token-modal": BuildTokenModal
    },
    data: function () {
        return {
            tokens: []
        }
    },
    created: function () {
        this.fetchTokens();

        system.events.$on("buildTokenModal:created", function (buildToken) {
            this.tokens.push(buildToken);
        }.bind(this));

        system.events.$on("buildTokenModal:updated", function (buildToken) {
            let index = this.tokens.findIndex(function (elm) {
                return elm.buildTokenId == buildToken.buildTokenId;
            });
            if (index >= 0) {
                this.tokens.splice(index, 1, buildToken);
            }
        }.bind(this));
    }, 
    methods: {
        fetchTokens: function () {
            BuildTokenService.getAll().then(function (data) {
                this.tokens = data;
            }.bind(this),
            function (error) {
                console.log("Error fetching build tokens: " + error)
            });

        },
        clear: function () {
            this.tokens = [];
        },
        create: function () {
            system.events.$emit("buildTokenModal:create");
        },
        edit: function (buildToken) {
            system.events.$emit("buildTokenModal:edit", buildToken);
        },
        deleteToken: function (buildToken) {
            return BuildTokenService.delete(buildToken.buildTokenId).then(function () {                  
                let index = this.tokens.indexOf(buildToken);
                this.tokens.splice(index, 1);                    
                return true;
            }.bind(this), function (error) {
                console.log("Error deleting build token: " + error);
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