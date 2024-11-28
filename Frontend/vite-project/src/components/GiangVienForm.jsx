import { zodResolver } from "@hookform/resolvers/zod";
import { set, useForm } from "react-hook-form";
import { z } from "zod";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { addGiangVien, updateGiangVien } from "@/api/api-giangvien";
// import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
  khoaId: z.coerce.number(
    {
      message: "Lop Hoc Phan Id must be a number",
    }
  ).min(1, {
    message: "Lop Hoc Phan Id must be at least 1 characters.",
  }),
});
 

export function GiangVienForm({ giangVienId, handleAdd, handleEdit, setIsDialogOpen }) {
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      id: giangVienId,
      ten: "",
      khoaId: "",
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    if (giangVienId) {
      const data = await updateGiangVien(giangVienId, values);
      handleEdit(data);
      console.log("Updating Giang Vien", values);
    } else {
      console.log("Add Giang Vien", values);
      const data = await addGiangVien(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {giangVienId && (
          <FormField
            control={form.control}
            name="id"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Id</FormLabel>
                <FormControl>
                  <Input {...field} readOnly/>
                </FormControl>
                <FormDescription>
                  This is your unique identifier.
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="ten"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Tên</FormLabel>
              <FormControl>
                <Input placeholder="Nguyễn Văn A" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="khoaId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>KhoaId</FormLabel>
              <FormControl>
              <Input placeholder="1" {...field} />
              </FormControl>
              <FormDescription>
                This is Khoa Id of this Giang Vien.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">Submit</Button>
      </form>
    </Form>
  );
}
