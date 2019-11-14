<template>
    <div>     
        <div class="columns">
            <div class="column">
                <p>
                    Here is the text on the left, that is really just here to make the form on the right look better.
                    But at some point there will most likely be some informational text here. But in all
                    honesty, I will probably be too lazy to populate it and it will stay like this. 
                    Hopefully this doesn't change your mind about registering.
                </p>
            </div>
            <div class="column">
                <div class="field">
                    <label>Name</label>
                    <div
                        class="control"
                        :class=" {'has-icons-right': nameEmpty }"
                    >
                        <input
                            v-model="name"
                            class="input"
                            type="text"
                            placeholder="Name" 
                            :class="{ 'is-danger': nameEmpty }"
                        >
                        <span
                            v-if="nameEmpty"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="nameEmpty"
                        class="help is-danger"
                    >
                        Name cannot be empty!
                    </p>
                </div>
                <div class="field">
                    <label>Username</label>
                    <div class="control has-icons-left has-icons-right">
                        <input
                            v-model="username"
                            class="input"
                            type="text"
                            placeholder="Username"
                            :class="{ 'is-danger': !usernameAvailable || usernameEmpty, 'is-success': usernameAvailable && username }"
                            @blur="validateUsernameAvailable()"
                        >
                        <span class="icon is-small is-left">
                            <app-icon
                                icon="user"
                                :action="false"
                            />
                        </span>
                        <template v-if="username">
                            <span class="icon is-small is-right">
                                <app-icon
                                    v-if="usernameAvailable"
                                    id="hi1"
                                    icon="check"
                                    :action="false"
                                />
                                <app-icon
                                    v-else
                                    id="hi2"
                                    icon="exclamation-triangle"
                                    :action="false"
                                />
                            </span>
                        </template>
                        <span
                            v-else
                            class="icon is-small is-right"
                        >
                            <app-icon
                                v-if="usernameEmpty"
                                id="hi2"
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <template v-if="username">
                        <p
                            v-if="usernameAvailable"
                            class="help is-success"
                        >
                            This username is available
                        </p>
                        <p
                            v-else
                            class="help is-danger"
                        >
                            This username is not available
                        </p>
                    </template>
                    <p
                        v-if="usernameEmpty"
                        class="help is-danger"
                    >
                        Username cannot be empty!
                    </p>
                </div>
                <div class="field">
                    <label>Password</label>
                    <div
                        class="control"
                        :class=" {'has-icons-right': !passwordsMatch || passwordEmpty }"
                    >
                        <input
                            v-model="password"
                            class="input"
                            type="password"
                            placeholder="Password" 
                            :class="{ 'is-danger': !passwordsMatch || passwordEmpty }"
                        >
                        <span
                            v-if="!passwordsMatch || passwordEmpty"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="passwordEmpty"
                        class="help is-danger"
                    >
                        Password cannot be empty!
                    </p>
                </div>
                <div class="field">
                    <label>Confirm Password</label>
                    <div
                        class="control"
                        :class=" {'has-icons-right': !passwordsMatch }"
                    >
                        <input
                            v-model="passwordConfirm"
                            class="input"
                            type="password"
                            placeholder="Password"
                            :class="{ 'is-danger': !passwordsMatch }"
                        >
                        <span
                            v-if="!passwordsMatch"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="!passwordsMatch"
                        class="help is-danger"
                    >
                        Passwords do not match!
                    </p>
                </div>

                <div class="field">
                    <label>Email</label>
                    <div
                        class="control has-icons-left"
                        :class=" {'has-icons-right': !emailsMatch || emailEmpty}"
                    >
                        <input
                            v-model="email"
                            class="input"
                            type="email"
                            placeholder="Email"
                            :class="{ 'is-danger': !emailsMatch || emailEmpty }"
                        >
                        <span class="icon is-small is-left">
                            <app-icon
                                icon="envelope"
                                :action="false"
                            />
                        </span>
                        <span
                            v-if="!emailsMatch || emailEmpty"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="emailEmpty"
                        class="help is-danger"
                    >
                        Email cannot be empty!
                    </p>
                </div>
                <div class="field">
                    <label>Confirm Email</label>
                    <div
                        class="control has-icons-left"
                        :class=" {'has-icons-right': !emailsMatch }"
                    >
                        <input
                            v-model="emailConfirm"
                            class="input"
                            type="email"
                            placeholder="Email"
                            :class="{ 'is-danger': !emailsMatch }"
                        >
                        <span class="icon is-small is-left">
                            <app-icon
                                icon="envelope"
                                :action="false"
                            />
                        </span>
                        <span
                            v-if="!emailsMatch"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="!emailsMatch"
                        class="help is-danger"
                    >
                        Emails do not match!
                    </p>
                </div>
                <div class="field">
                    <label>Registration Key</label>
                    <div
                        class="control has-icons-left"
                        :class="{'has-icons-right': registrationKeyEmpty }"
                    >
                        <input
                            v-model="registrationKey"
                            class="input"
                            type="text"
                            placeholder="Registration Key" 
                            :class="{ 'is-danger': registrationKeyEmpty }"
                        >
                        <span class="icon is-small is-left">
                            <app-icon
                                icon="key"
                                :action="false"
                            />
                        </span>
                        <span
                            v-if="registrationKeyEmpty"
                            class="icon is-small is-right"
                        >
                            <app-icon
                                icon="exclamation-triangle"
                                :action="false"
                            />
                        </span>
                    </div>
                    <p
                        v-if="registrationKeyEmpty"
                        class="help is-danger"
                    >
                        Registration Key cannot be empty!
                    </p>
                </div>

                <div class="field is-grouped is-grouped-right">
                    <p class="control">
                        <button
                            class="button is-link"
                            @click="register"
                        >
                            Register
                        </button>
                    </p>
                    <p class="control">
                        <button
                            class="button"
                            @click="cancel"
                        >
                            Cancel
                        </button>
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {UserService} from "@app/services/ApplicationProxy.js"
import Links from "@app/services/Links.js"
import util from "@app/services/Util.js"

import Icon from "@app/components/Common/Icon.vue"

export default {
    components: {
        "app-icon": Icon
    },
    data: function() {
        return {
            name: "",
            username: "",
            password: "",
            passwordConfirm: "",
            email: "",
            emailConfirm: "",
            registrationKey: "",

            //Validation stuff
            usernameAvailable: true,       
            clickedRegister: false
        }
    },
    computed: {
        passwordsMatch: function () {
            if (util.isStringNullOrBlank(this.password) || util.isStringNullOrBlank(this.passwordConfirm)) {
                return true;
            }
            return this.password === this.passwordConfirm;
        },
        emailsMatch: function () {
            if (util.isStringNullOrBlank(this.email) || util.isStringNullOrBlank(this.emailConfirm)) {
                return true;
            }
            return this.email === this.emailConfirm;
        },
        nameEmpty: function () {
            return this.clickedRegister && util.isStringNullOrBlank(this.name);
        },
        usernameEmpty: function () {
            return this.clickedRegister && util.isStringNullOrBlank(this.username);
        },
        passwordEmpty: function () {
            return this.clickedRegister && util.isStringNullOrBlank(this.password);
        },
        emailEmpty: function () {
            return this.clickedRegister && util.isStringNullOrBlank(this.email);
        },
        registrationKeyEmpty: function () {
            return this.clickedRegister && util.isStringNullOrBlank(this.registrationKey);
        }
    },
    methods: {
        createModel: function () {
            return {
                name: this.name,
                username: this.username,
                password: this.password,
                email: this.email,
                registrationKey: this.registrationKey
            };
        },
        register: function () {
            this.clickedRegister = true;
            if (this.nameEmpty || this.usernameEmpty || this.passwordEmpty ||
                this.emailEmpty || this.registrationKeyEmpty ||
                !this.passwordsMatch || !this.emailsMatch) {             
                return;
            }             

            UserService.register(this.createModel()).then(function (res) {
                if (res) {
                    window.location = Links.login + "?registered";
                }
                else {
                    console.log("Failed to register. Error occured");
                }
            },
            function (error) {
                console.log("Error occured: " + error)
            });
        },
        cancel: function () {
            window.location = Links.home;
        },
        validateUsernameAvailable: function () {
            UserService.usernameAvailable(this.username).then(function (res) {
                this.usernameAvailable = res === true;                    
            }.bind(this));
        }
    }
}
</script>