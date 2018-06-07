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
            <div class="column"></div>
            <div class="column">
                <application-list></application-list>
            </div>
        </div>
    </div>   
</template>

<script>
    import ApplicationList from "components/Application/ApplicationList.vue"

    export default {
        data: function() {
            return {
                "message": "Welcome again"
            }
        },
        components: {
            ApplicationList
        }
    }
</script>