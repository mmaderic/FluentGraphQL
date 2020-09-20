CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE TABLE "public"."customer"("id" uuid NOT NULL DEFAULT gen_random_uuid(), "first_name" text NOT NULL, "last_name" text NOT NULL, "phone" text, "email" text NOT NULL, "street" text, "city" text, "zip_code" text, "country" text, PRIMARY KEY ("id") );
