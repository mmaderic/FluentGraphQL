alter table "public"."staff"
           add constraint "staff_manager_id_fkey"
           foreign key ("manager_id")
           references "public"."staff"
           ("id") on update no action on delete no action;
