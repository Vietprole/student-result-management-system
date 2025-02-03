import React, { useState } from 'react';
import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View, SafeAreaView, Platform, StatusBar, Image } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import { SelectList } from 'react-native-dropdown-select-list';
import { apiClient } from '../api/apiClient';

const handleFooterPress = (route: string) => {
  if (route === 'home') {
    router.push('/(tabs)/Home');
  } else if (route === 'person') {
    router.push('/(tabs)/Profile');
  }
};

type TableRow = {
  TT: string;
  MaHocPhan: string;
  HocPhan: string;
  SoTC: string;
  GV: string;
  TKB: string;
  TuanHoc: string;
};

type ColumnWidths = {
  [key: string]: number;
};

const Result = () => {
  const [selected, setSelected] = React.useState("");
  const tableHeaders = ['TT', 'Mã học phần', 'Tên lớp học phần', 'Số tín chỉ', 'Giảng viên', 'Thời khoá biểu', 'Tuần học'];
  const tableData: TableRow[] = [
    { TT: '1', MaHocPhan: '1020413.2410.21.11', HocPhan: 'Kiểm thử phần mềm	', SoTC: '2', GV: 'Võ Đức Hoàng	', TKB: 'Thứ 5,9-10,B303', TuanHoc: '3-18	' },
    { TT: '2', MaHocPhan: '1023780.2410.21.11B	', HocPhan: 'PBL 6: Dự án CN Công nghệ phần mềm', SoTC: '4', GV: 'Đặng Thiên Bình', TKB: 'Thứ 7,7-10,B105', TuanHoc: '3-18' },
  ];
  const semesters = [
    { key: '1', value: 'Học kỳ 1 - Năm 2024-2025' },
    { key: '2', value: 'Học kỳ 2 - Năm 2023-2024' },
    { key: '3', value: 'Học kỳ 1 - Năm 2023-2024' },
  ];
  const handleSelect = () => {
    if (selected) {
      console.log("Selected semester:", selected);
    } else {
      Alert.alert("Thông báo", "Vui lòng chọn học kỳ");
    }
  };
  const columnWidths: ColumnWidths = {
    TT: 50,
    MaHocPhan: 180,
    HocPhan: 280,
    SoTC: 100,
    GV: 150,
    TKB: 150,
    TuanHoc: 100,
  };

  const headerToWidthKey: { [key: string]: string } = {
    'TT': 'TT',
    'Mã học phần': 'MaHocPhan',
    'Tên lớp học phần': 'HocPhan',
    'Số tín chỉ': 'SoTC',
    'Giảng viên': 'GV',
    'Thời khoá biểu': 'TKB',
    'Tuần học': 'TuanHoc',
  };

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: columnWidths[headerToWidthKey[header]] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cellTT, { width: columnWidths.TT }]}>{item.TT}</Text>
      <Text style={[styles.cell, { width: columnWidths.MaHocPhan }]}>{item.MaHocPhan}</Text>
      <Text style={[styles.cell, { width: columnWidths.HocPhan }]}>{item.HocPhan}</Text>
      <Text style={[styles.cell, { width: columnWidths.SoTC }]}>{item.SoTC}</Text>
      <Text style={[styles.cell, { width: columnWidths.GV }]}>{item.GV}</Text>
      <Text style={[styles.cell, { width: columnWidths.TKB }]}>{item.TKB}</Text>
      <Text style={[styles.cell, { width: columnWidths.TuanHoc }]}>{item.TuanHoc}</Text>
    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <View style={styles.header}>
          <View style={styles.headerLeft}>
            <Image
              source={{ uri: 'https://i.imghippo.com/files/mUo4100yA.webp' }}
              style={styles.headerLogo}
            />
          </View>
          <Text style={styles.logoText}>THỜI KHOÁ BIỂUx</Text>
          <Ionicons name="notifications-outline" size={24} color="white" />
        </View>

        {/* Add Dropdown Section */}
        <View style={styles.dropdownContainer}>
          <SelectList
            setSelected={setSelected}
            data={semesters}
            placeholder="Chọn học kỳ"
            searchPlaceholder="Tìm học kỳ"
            save="value"
            boxStyles={styles.dropdown}
            dropdownStyles={styles.dropdownList}
          />
          <TouchableOpacity
            style={styles.selectButton}
            onPress={handleSelect}
          >
            <Text style={styles.selectButtonText}>Chọn</Text>
          </TouchableOpacity>
        </View>

        {/* Table Content */}
        <ScrollView horizontal>
          <View style={styles.table}>
            {renderHeader()}
            <FlatList
              data={tableData}
              renderItem={renderRow}
              keyExtractor={(item, index) => index.toString()}
            />
          </View>
        </ScrollView>

        {/* Footer */}
        <View style={styles.footer}>
          {footerItems.map((item, index) => (
            <TouchableOpacity
              key={index}
              style={styles.footerItem}
              onPress={() => handleFooterPress(item.icon)}
            >
              <Ionicons name={item.icon} size={24} color="#0000cc" />
              <Text style={styles.footerText}>{item.label}</Text>
            </TouchableOpacity>
          ))}
        </View>
      </View>
    </SafeAreaView>
  );
};

const footerItems: { icon: 'home' | 'person', label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    borderRadius: 8,
    overflow: 'hidden',
    margin: 16,
    backgroundColor: 'white',
  },
  row: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  cellTT: {
    padding: 10,
    textAlign: 'center',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  cell: {
    padding: 10,
    textAlign: 'left',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
    backgroundColor: '#f1f1f1',
    textAlign: 'center',
    paddingVertical: 15,
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
    textAlign: 'center',
  },
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
    paddingTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerLogo: {
    width: 50,
    height: 50,
    marginRight: 10,
  },
  logoText: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: 'white',
    paddingVertical: 10,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
  },
  footerItem: {
    alignItems: 'center',
  },
  footerText: {
    marginTop: 5,
    fontSize: 12,
    color: '#0000cc',
  },
  dropdownContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    gap: 10,
  },
  dropdown: {
    flex: 1,
    borderColor: '#0000cc',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 12,
  },
  dropdownList: {
    borderColor: '#0000cc',
    marginTop: 5,
    maxHeight: 150,
  },
  selectButton: {
    backgroundColor: '#0000cc',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    height: 43,
  },
  selectButtonText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
});

export default Result;