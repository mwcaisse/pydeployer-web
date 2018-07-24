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
                    <input class="input" type="text" v-model="name" placeholder="Name" />
                </div>
                <div class="field">
                    <label>Username</label>
                    <div class="control has-icons-left has-icons-right">
                        <input class="input" type="text" placeholder="Username" 
                               v-model="username"  v-on:blur="validateUsernameAvailable()"
                               v-bind:class="{ 'is-danger': !usernameAvailable }"/>
                        <span class="icon is-small is-left">
                            <app-icon icon="user" :action="false"></app-icon>
                        </span>
                        <template v-if="username">
                            <span class="icon is-small is-right" >
                                <app-icon id="hi1" icon="check" :action="false" v-if="usernameAvailable"></app-icon>                 
                                <app-icon id="hi2" icon="exclamation-triangle" :action="false" v-else></app-icon>
                            </span>
                        </template>
                    </div>
                    <template v-if="username">
                        <p class="help is-success" v-if="usernameAvailable">This username is available</p>
                        <p class="help is-danger" v-else>This username is not available</p>
                    </template>
                </div>
                <div class="field">
                    <label>Password</label>
                    <input class="input" type="password" v-model="password" placeholder="Password" />
                </div>
                <div class="field">
                    <label>Confirm Password</label>
                    <input class="input" type="password" v-model="passwordConfirm" placeholder="Password" />
                    <p class="help is-danger" v-if="!passwordsMatch">Passwords do not match!</p>
                </div>

                <div class="field">
                    <label>Email</label>
                    <div class="control has-icons-left">
                        <input class="input" type="email" v-model="email" placeholder="Email" />
                        <span class="icon is-small is-left">
                            <app-icon icon="envelope" :action="false"></app-icon>
                        </span>
                    </div>
                </div>
                <div class="field">
                    <label>Confirm Email</label>
                    <input class="input" type="email" v-model="emailConfirm" placeholder="Email" />
                    <p class="help is-danger" v-if="!emailsMatch">Emails do not match!</p>
                </div>
                <div class="field">
                    <label>Registration Key</label>
                    <div class="control has-icons-left">
                        <input class="input" type="text" v-model="registrationKey" placeholder="Registration Key" />
                        <span class="icon is-small is-left">
                            <app-icon icon="key" :action="false"></app-icon>
                        </span>
                    </div>                 
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
    import { UserService } from "services/ApplicationProxy.js"
    import util from "services/Util.js"

    import Icon from "components/Common/Icon.vue"

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

                // Validation stuff
                usernameAvailable: true,
                usernameEmpty: false,
                registrationKeyEmpty: false
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
                if (!this.passwordsMatch) {
                    alert("Passwords must match")
                    return;
                }
                if (!this.emailsMatch) {
                    alert("Emails must match!");
                    return;
                }

                UserService.register(this.createModel()).then(function (res) {
                    if (res) {
                        window.location = "/login?registered"; // TODO: Change this up
                    }
                    else {
                        alert("Failed to register. Error occured");
                    }
                }.bind(this),
                function (error) {
                    alert("Error occured: " + error)
                });
            },
            cancel: function () {
                window.location = "/";
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