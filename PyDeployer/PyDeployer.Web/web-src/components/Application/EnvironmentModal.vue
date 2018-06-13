<template>
    <div class="modal" v-show="show">
        <div class="modal-background"></div>
        <div class="modal-card">
            <header class="modal-card-header">
                <p class="modal-card-title">Select Environment</p>
                <button class="delete" aria-label="close" v-on:click="close"></button>
            </header>
            <section class="modal-card-body">
                <ul>
                    <li class="box" v-for="environment in environments">
                        {{environment.name}}                 
                    </li>
                </ul>
            </section>
            <footer class="modal-card-foot">
                <button class="button" type="button" v-on:click="close">Cancel</button>
            </footer>
        </div>      
    </div>
</template>

<script>
    import system from "services/System.js"
    import { EnvironmnetService } from "services/ApplicationProxy.js"

    export default {
        name: "environment-modal",
        data: function() {
            return {
                show: false,
                environments: []
            }
        },
        methods: {
            fetchEnvironments: function () {
                EnvironmnetService.getAll().then(function (data) {
                    this.environments = data;
                }.bind(this),
                function (error) {
                    console.log("Error fetching environments: " + error)
                });
            },
            close: function () {
                this.show = false;
            }
        },
        created: function () {
            this.fetchEnvironments();

            console.log(system);


            system.events.$on("environmentModal:show", function () {
                this.show = true
                console.log("Recieved show modal event");
                console.log(this)
            }.bind(this));

            system.events.$on("environmentModal:hide", function () {
                this.show = false
            }.bind(this));
        }
    }
</script>
