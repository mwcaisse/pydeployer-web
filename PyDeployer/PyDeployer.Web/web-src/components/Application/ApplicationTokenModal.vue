<template>
    <div class="modal" v-bind:class="{'is-active': show }">
        <div class="modal-background" v-on:click="close"></div>
        <div class="modal-card">
            <header class="modal-card-head">
                <p class="modal-card-title">Select Environment</p>
                <button class="delete" aria-label="close" v-on:click="close"></button>
            </header>
            <section class="modal-card-body">
                <ul>
                    <li class="box action" v-for="environment in environments" v-on:click="selectEnvironment(environment)">
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
            },
            selectEnvironment: function (environment) {
                this.$emit("environment:selected", environment);
                this.close();
            }
        },
        created: function () {
            this.fetchEnvironments();

            system.events.$on("environmentModal:show", function () {
                this.show = true
            }.bind(this));

            system.events.$on("environmentModal:hide", function () {
                this.show = false
            }.bind(this));
        }
    }
</script>

<style scoped>
    .box.action:hover {
        background-color: rgba(299, 40, 69, 0.7);
        cursor: pointer
    }
</style>
