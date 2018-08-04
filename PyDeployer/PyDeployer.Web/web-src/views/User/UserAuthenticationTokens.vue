<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Authentication Tokens</h1>
            </div>
        </section>
        <section class="section">
            <user-authentication-token-list></user-authentication-token-list>
        </section>
    </div>
</template>

<script>
    import UserAuthenticationTokenList from "components/User/AuthenticationTokenList.vue" 

    export default {
        data: function () {
            return {             
            }
        },
        components: {
            UserAuthenticationTokenList
        }
    }
</script>
