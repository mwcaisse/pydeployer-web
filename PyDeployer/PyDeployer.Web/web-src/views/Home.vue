<template>
    <div>       
        <section class="section">
            <div class="container">
                <h1 class="title">
                    PyDeployer
                </h1>
                <p class="subtitle">
                    Welcome to PyDeployer!
                </p>
            </div>
        </section> 
        <div class="columns">
            <div class="column">
                <environment-list />
            </div>
            <div class="column">
                <application-list />
            </div>
        </div>
    </div>   
</template>

<script>
import ApplicationList from "@app/components/Application/ApplicationList.vue"
import EnvironmentList from "@app/components/Environment/EnvironmentList.vue"

export default {
    components: {
        "application-list": ApplicationList,
        "environment-list": EnvironmentList
    },
    data: function() {
        return {
            "message": "Welcome again"
        }
    }
}
</script>