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
import { addSinhVien, updateSinhVien } from "@/api/api-sinhvien";
// import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
});

export function SinhVienForm({ sinhVienId, handleAdd, handleEdit, setIsDialogOpen }) {
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      id: sinhVienId,
      ten: "",
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    if (sinhVienId) {
      const data = await updateSinhVien(sinhVienId, values);
      handleEdit(data);
      console.log("Updating sinh vien", values);
    } else {
      const data = await addSinhVien(values);
      handleAdd(data);
      console.log("Add sinh vien", values);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {sinhVienId && (
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
        <Button type="submit">Submit</Button>
      </form>
    </Form>
  );
}
