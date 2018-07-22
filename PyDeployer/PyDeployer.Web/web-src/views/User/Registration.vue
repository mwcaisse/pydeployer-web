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
                    <input class="input" type="text" v-model="username" placeholder="Username" />
                </div>
                <div class="field">
                    <label>Password</label>
                    <input class="input" type="password" v-model="password" placeholder="Password" />
                </div>
                <div class="field">
                    <label>Confirm Password</label>
                    <input class="input" type="password" v-model="passwordConfirm" placeholder="Password" />
                </div>

                <div class="field">
                    <label>Email</label>
                    <input class="input" type="email" v-model="email"placeholder="Email" />
                </div>
                <div class="field">
                    <label>Confirm Email</label>
                    <input class="input" type="email" v-model="emailConfirm" placeholder="Email" />
                </div>
                <div class="field">
                    <label>Registration Key</label>
                    <input class="input" type="text" v-model="registrationKey" placeholder="Registration Key" />
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
                return this.password === this.passwordConfirm;
            },
            emailsMatch: function () {
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
                        window.location = "/login?source=registered"; // TODO: Change this up
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
            }
        }
    }
</script>