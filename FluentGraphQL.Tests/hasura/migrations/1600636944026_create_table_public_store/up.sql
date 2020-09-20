CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE TABLE "public"."store"("id" uuid NOT NULL DEFAULT gen_random_uuid(), "name" text NOT NULL, "phone" text, "email" text, "street" text, "city" text, "zip_code" text, PRIMARY KEY ("id") );
