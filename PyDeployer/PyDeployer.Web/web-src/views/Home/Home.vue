<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">PyDeployer</h1>
                <p class="subtitle">
                    Welcome to PyDeployer!
                </p>
            </div>
        </section> 
        <div class="columns">
            <div class="column">
                <environment-list></environment-list>
            </div>
            <div class="column">
                <application-list></application-list>
            </div>
        </div>
    </div>   
</template>

<script>
    import ApplicationList from "components/Application/ApplicationList.vue"
    import EnvironmentList from "components/Environment/EnvironmentList.vue"

    export default {
        data: function() {
            return {
                "message": "Welcome again"
            }
        },
        components: {
            "application-list": ApplicationList,
            "environment-list": EnvironmentList
        }
    }
</script>