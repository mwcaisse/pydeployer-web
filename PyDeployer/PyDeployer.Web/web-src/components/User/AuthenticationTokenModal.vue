<template>
    <app-modal ref="modal" title="Create Authentication Token">
        <div class="field" v-if="!tokenValue">
            <label class="label">Description</label>
            <div class="control">
                <input class="input" type="text" placeholder="Description" v-model="description" />
            </div>
        </div>
        <div v-else>
            <span>Your token is below:</span>
            <div class="box">
                <p class="has-text-centered">
                    {{ tokenValue }}
                </p>
            </div>
        </div>
        <template slot="footer-buttons">
            <button class="button" type="button" v-on:click="create">Create</button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {UserAuthenticationTokenService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "application-token-modal",
    data: function () {
        return {       
            description: "",
            tokenValue: ""
        }
    },
    methods: {      
        create: function () {          
            return UserAuthenticationTokenService.create(this.description).then(function (tokenValue) {
                system.events.$emit("authenticationTokenModal:created");
                this.tokenValue = tokenValue;
                return true;
            }.bind(this), function (error) {
                console.log("Error saving authentication token: " + error)
                return false;
            });
        },            
        close: function () {
            this.$refs.modal.close();
        },     
        clear: function () {  
            this.description = "";
            this.tokenValue = "";
        }    
    },
    created: function () {
        system.events.$on("authenticationTokenModal:create", function () {
            this.clear();     
            this.$refs.modal.open();
        }.bind(this));   

        system.events.$on("authenticationTokenModal:hide", function () {
            this.close();
        }.bind(this));
    },
    components: {
        "app-modal": Modal
    }
}
</script>
