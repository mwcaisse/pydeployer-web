<template>
    <div>      
        <user-authentication-token-list />  
    </div>
</template>

<script>
import UserAuthenticationTokenList from "@app/components/User/AuthenticationTokenList.vue" 

export default {
    components: {
        UserAuthenticationTokenList
    },
    data: function () {
        return {             
        }
    }
}
</script>
