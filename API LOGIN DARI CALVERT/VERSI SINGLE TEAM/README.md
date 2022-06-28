**NOTE:** sql e teko ojs.petra.ac.id Collation e tak ubah teko **utf8mb4_0900_ai_ci** jadi **utf8mb4_general_ci** ben iso jalan nang local ku, mungkin nek mau dilebokno server harus dibalikno maneh collation e

**NOTE:** sql e teko ojs.petra.ac.id Collation e tak ubah teko **utf8mb4_0900_ai_ci** jadi **utf8mb4_general_ci** ben iso jalan nang local ku, mungkin nek mau dilebokno server harus dibalikno maneh collation e

1. Import sql e sek ke database
2. Hit API login e nang

**http://localhost/<jenenge_folder_iki>/api/login.php**

**Request body gae testing:**
name= **dummy**
password = **dummy**

---

**Responses**

**200, POST - Successfully logged in**

```
{
	"success": true
	"message": "Successfully logged in"
}
```

**400, POST - Missing credentials**

```
{
	"success": false
	"message": "Missing credentials"
}
```

**400, POST - Already logged in**

```
{
	"success": false
	"message": "Already logged in"
}
```

**401, POST - Wrong credentials**

```
{
	"success": false
	"message": "Wrong credentials"
}
```

1. Import sql e sek ke database
2. Hit API login e nang

**http://localhost/<jenenge_folder_iki>/api/login.php**

**Request body gae testing:**
name = **dummy**
password = **dummy**

**Response:**

```
{
	"success": false / true
	"message": "..."
}
```

**Status Code:**
**200** nek success
**400** nek missing fields (name / password)
**401** nek wrong credentials
