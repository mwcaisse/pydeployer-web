<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Application Details</h1>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <application-details :applicationId="applicationId"></application-details>
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <application-token-list :applicationId="applicationId"></application-token-list>
            </div>
            <div class="column">
                <application-environment-list :applicationId="applicationId"></application-environment-list>
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <application-environment-tokens :applicationId="applicationId"></application-environment-tokens>
            </div>
        </div>
    </div> 
</template>

<script>
    import util from "services/Util.js"
    import ApplicationDetails from "components/Application/ApplicationDetails.vue"
    import ApplicationEnvironmentList from "components/Application/ApplicationEnvironmentList.vue"
    import ApplicationTokenList from "components/Application/ApplicationTokenList.vue"
    import ApplicationEnvironmentTokens from "components/Application/ApplicationEnvironmentTokens.vue"

    var applicationId = parseInt(util.getURLParameter("applicationId", -1), 10)

    export default {
        data: function() {
            return {
                applicationId: applicationId
            }
        },
        components: {
            ApplicationDetails,
            ApplicationEnvironmentList,
            ApplicationTokenList,
            ApplicationEnvironmentTokens
        }
    }
</script>