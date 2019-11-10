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
import ApplicationDetails from "@app/components/Application/ApplicationDetails.vue"
import ApplicationEnvironmentList from "@app/components/Application/ApplicationEnvironmentList.vue"
import ApplicationTokenList from "@app/components/Application/ApplicationTokenList.vue"
import ApplicationEnvironmentTokens from "@app/components/Application/ApplicationEnvironmentTokens.vue"

const applicationId = parseInt($("#applicationId").val(), 10);

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