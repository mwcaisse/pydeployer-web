<template>
    <app-modal
        ref="modal"
        title="Create Authentication Token"
    >
        <div
            v-if="!tokenValue"
            class="field"
        >
            <label class="label">Description</label>
            <div class="control">
                <input
                    v-model="description"
                    class="input"
                    type="text"
                    placeholder="Description"
                >
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
            <button
                class="button"
                type="button"
                @click="create"
            >
                Create
            </button>
        </template>
    </app-modal>
</template>

<script>
import system from "@app/services/System.js"
import {UserAuthenticationTokenService} from "@app/services/ApplicationProxy.js"

import Modal from "@app/components/Common/Modal.vue"

export default {
    name: "ApplicationTokenModal",
    components: {
        "app-modal": Modal
    },
    data: function () {
        return {       
            description: "",
            tokenValue: ""
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
    }
}
</script>
