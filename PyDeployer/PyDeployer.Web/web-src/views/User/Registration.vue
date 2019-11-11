<template>
    <div>     
        <div class="columns">
            <div class="column">
                <p>Here is the text on the left, that is really just here to make the form on the right look better.
                    But at some point there will most likely be some informational text here. But in all
                    honesty, I will probably be too lazy to populate it and it will stay like this. 
                    Hopefully this doesn't change your mind about registering.</p>
            </div>
            <div class="column">
                <div class="field">
                    <label>Name</label>
                    <div class="control" v-bind:class=" {'has-icons-right': nameEmpty }">
                        <input class="input" type="text" v-model="name" placeholder="Name" 
                               v-bind:class="{ 'is-danger': nameEmpty }" />
                        <span class="icon is-small is-right" v-if="nameEmpty">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="nameEmpty">Name cannot be empty!</p>
                </div>
                <div class="field">
                    <label>Username</label>
                    <div class="control has-icons-left has-icons-right">
                        <input class="input" type="text" placeholder="Username"
                               v-model="username" v-on:blur="validateUsernameAvailable()"
                               v-bind:class="{ 'is-danger': !usernameAvailable || usernameEmpty, 'is-success': usernameAvailable && username }" />
                        <span class="icon is-small is-left">
                            <app-icon icon="user" :action="false"></app-icon>
                        </span>
                        <template v-if="username">
                            <span class="icon is-small is-right">
                                <app-icon id="hi1" icon="check" :action="false" v-if="usernameAvailable"></app-icon>
                                <app-icon id="hi2" icon="exclamation-triangle" :action="false" v-else></app-icon>
                            </span>
                        </template>
                        <span class="icon is-small is-right" v-else>
                            <app-icon id="hi2" icon="exclamation-triangle" :action="false" v-if="usernameEmpty"></app-icon>
                        </span>
                    </div>
                    <template v-if="username">
                        <p class="help is-success" v-if="usernameAvailable">This username is available</p>
                        <p class="help is-danger" v-else>This username is not available</p>
                    </template>
                    <p class="help is-danger" v-if="usernameEmpty">Username cannot be empty!</p>
                </div>
                <div class="field">
                    <label>Password</label>
                    <div class="control" v-bind:class=" {'has-icons-right': !passwordsMatch || passwordEmpty }">
                        <input class="input" type="password" v-model="password" placeholder="Password" 
                               v-bind:class="{ 'is-danger': !passwordsMatch || passwordEmpty }" />
                        <span class="icon is-small is-right" v-if="!passwordsMatch || passwordEmpty">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="passwordEmpty">Password cannot be empty!</p>
                </div>
                <div class="field">
                    <label>Confirm Password</label>
                    <div class="control" v-bind:class=" {'has-icons-right': !passwordsMatch }">
                        <input class="input" type="password" v-model="passwordConfirm" placeholder="Password"
                               v-bind:class="{ 'is-danger': !passwordsMatch }" />
                        <span class="icon is-small is-right" v-if="!passwordsMatch">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="!passwordsMatch">Passwords do not match!</p>
                </div>

                <div class="field">
                    <label>Email</label>
                    <div class="control has-icons-left" v-bind:class=" {'has-icons-right': !emailsMatch || emailEmpty}">
                        <input class="input" type="email" v-model="email" placeholder="Email"
                               v-bind:class="{ 'is-danger': !emailsMatch || emailEmpty }" />
                        <span class="icon is-small is-left">
                            <app-icon icon="envelope" :action="false"></app-icon>
                        </span>
                        <span class="icon is-small is-right" v-if="!emailsMatch || emailEmpty">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="emailEmpty">Email cannot be empty!</p>
                </div>
                <div class="field">
                    <label>Confirm Email</label>
                    <div class="control has-icons-left" v-bind:class=" {'has-icons-right': !emailsMatch }">
                        <input class="input" type="email" v-model="emailConfirm" placeholder="Email"
                               v-bind:class="{ 'is-danger': !emailsMatch }" />
                        <span class="icon is-small is-left">
                            <app-icon icon="envelope" :action="false"></app-icon>
                        </span>
                        <span class="icon is-small is-right" v-if="!emailsMatch">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="!emailsMatch">Emails do not match!</p>
                </div>
                <div class="field">
                    <label>Registration Key</label>
                    <div class="control has-icons-left" v-bind:class="{'has-icons-right': registrationKeyEmpty }">
                        <input class="input" type="text" v-model="registrationKey" placeholder="Registration Key" 
                               v-bind:class="{ 'is-danger': registrationKeyEmpty }" />
                        <span class="icon is-small is-left">
                            <app-icon icon="key" :action="false"></app-icon>
                        </span>
                        <span class="icon is-small is-right" v-if="registrationKeyEmpty">
                            <app-icon icon="exclamation-triangle" :action="false"></app-icon>
                        </span>
                    </div>
                    <p class="help is-danger" v-if="registrationKeyEmpty">Registration Key cannot be empty!</p>
                </div>

                <div class="field is-grouped is-grouped-right">
                    <p class="control">
                        <button class="button is-link" v-on:click="register">Register</button>
                    </p>
                    <p class="control">
                        <button class="button" v-on:click="cancel">Cancel</button>
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
    },
    components: {
        "app-icon": Icon
    }
}
</script>