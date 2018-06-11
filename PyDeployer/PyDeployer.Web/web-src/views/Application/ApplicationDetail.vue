<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Application Details</h1>
                <p class="subtitle">
                    Details of Test Application 1
                </p>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <application-details applicationId="1"></application-details>
            </div>
            <div class="column">
                <application-environment-list applicationId="1"></application-environment-list>
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <application-token-list applicationId="1"></application-token-list>
            </div>
            <div class="column">
                <span>Placeholder for the Token values per environment.</span>
            </div>
        </div>
    </div> 
</template>

<script>

    import ApplicationDetails from "components/Application/ApplicationDetails.vue"
    import ApplicationEnvironmentList from "components/Application/ApplicationEnvironmentList.vue"
    import ApplicationTokenList from "components/Application/ApplicationTokenList.vue"


    export default {
        data: function() {
            return {
                "message": "Welcome again"
            }
        },
        components: {
            ApplicationDetails,
            ApplicationEnvironmentList,
            ApplicationTokenList
        }
    }
</script>