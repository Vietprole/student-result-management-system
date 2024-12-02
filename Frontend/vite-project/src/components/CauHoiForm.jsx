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
import { addCauHoi, updateCauHoi } from "@/api/api-cauhoi";
// import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
  trongSo: z.string()
  .refine((val) => !isNaN(parseFloat(val)), {
    message: "Trong so must be a number",
  })
  .refine((val) => parseFloat(val) > 0.1 && parseFloat(val) < 10, {
    message: "Trong so must be between 0.1 and 10",
  }),
  baiKiemTraId: z.coerce.number(
    {
      message: "Bai Kiem Tra Id must be a number",
    }
  ).min(1, {
    message: "Bai Kiem Tra Id must be at least 1 characters.",
  }),
});

export function CauHoiForm({ cauHoiId, handleAdd, handleEdit, setIsDialogOpen }) {
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      id: cauHoiId,
      ten: "",
      trongSo: "",
      baiKiemTraId: "",
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    if (cauHoiId) {
      const data = await updateCauHoi(cauHoiId, values);
      handleEdit(data);
    } else {
      const data = await addCauHoi(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {cauHoiId && (
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
                <Input placeholder="Câu 1a" {...field} />
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
          name="trongSo"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Trọng Số</FormLabel>
              <FormControl>
                <Input placeholder="1.5" {...field} />
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
          name="baiKiemTraId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>ID Bài Kiểm Tra</FormLabel>
              <FormControl>
                <Input placeholder="1" {...field} />
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
