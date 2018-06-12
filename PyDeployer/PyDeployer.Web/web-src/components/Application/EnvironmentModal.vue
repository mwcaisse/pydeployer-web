<template>
    <div class="modal">
        <div class="modal-background"></div>
        <div class="modal-card">
            <header class="modal-card-header">
                <p class="modal-card-title">Select Environment</p>
                <button class="delete" aria-label="close"></button>
            </header>
            <section class="modal-card-body">
                <ul>
                    <li class="box" v-for="environment in environments">
                        {{environment.name}}                 
                    </li>
                </ul>
            </section>
            <footer class="modal-card-foot">
                <button class="button" type="button">Cancel</button>
            </footer>
        </div>      
    </div>
</template>

<script>
    import { EnvironmnetService } from "services/ApplicationProxy.js"

    export default {
        name: "application-list",
        data: function() {
            return {
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
            }
        },
        created: function () {
            this.fetchEnvironments();
        }
    }
</script>
